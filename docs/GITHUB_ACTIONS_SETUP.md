# GitHub Actions CI/CD Setup Guide

## ✅ Step 1: Workflow Files Created

All workflow files have been created in `.github/workflows/`:
- ✅ `deploy-azure-container-apps.yml` - Builds and deploys APIs
- ✅ `migrate-azure-database.yml` - Runs database migrations
- ✅ `deploy-azure-angular.yml` - Deploys Angular frontend

---

## 🔐 Step 2: Create Service Principal and Add GitHub Secrets

### 2.1: Create Azure Service Principal

Run this command in PowerShell (replace with your subscription ID if needed):

```powershell
# Get your subscription ID
$SUBSCRIPTION_ID = az account show --query id -o tsv
Write-Host "Subscription ID: $SUBSCRIPTION_ID" -ForegroundColor Cyan

# Create service principal
az ad sp create-for-rbac --name "github-actions-poc" `
  --role contributor `
  --scopes /subscriptions/$SUBSCRIPTION_ID/resourceGroups/poc-deployment-rg `
  --sdk-auth
```

**Important**: Copy the entire JSON output! It will look like this:
```json
{
  "clientId": "xxxxx-xxxx-xxxx-xxxx-xxxxx",
  "clientSecret": "xxxxx-xxxx-xxxx-xxxx-xxxxx",
  "subscriptionId": "xxxxx-xxxx-xxxx-xxxx-xxxxx",
  "tenantId": "xxxxx-xxxx-xxxx-xxxx-xxxxx",
  ...
}
```

### 2.2: Add Secrets to GitHub

1. Go to your GitHub repository: `https://github.com/vasanth32/azure-deployment-poc`
2. Click **Settings** → **Secrets and variables** → **Actions**
3. Click **New repository secret** and add these secrets:

| Secret Name | Value | Description |
|------------|-------|-------------|
| `AZURE_CREDENTIALS` | (Paste the entire JSON from Step 2.1) | Service Principal for Azure authentication |
| `SQL_PASSWORD` | `P@ssw0rd123!` | SQL Server admin password |
| `AZURE_STATIC_WEB_APPS_API_TOKEN` | (Get from Static Web App - see below) | Only needed for Angular deployment |

**To get Static Web App token** (if you haven't created it yet):
```powershell
# After creating Static Web App, get the token
az staticwebapp secrets list `
  --name poc-angular-app `
  --resource-group poc-deployment-rg
```

---

## 📝 Step 3: Commit and Push Workflow Files

Run these commands:

```powershell
# Check git status
git status

# Add workflow files
git add .github/workflows/

# Commit
git commit -m "Add GitHub Actions CI/CD workflows for Azure Container Apps"

# Push to GitHub
git push origin main
```

---

## 🚀 Step 4: Run Migration Workflow (First Time)

After pushing, run the migration workflow manually:

1. Go to your GitHub repository: `https://github.com/vasanth32/azure-deployment-poc`
2. Click the **Actions** tab
3. In the left sidebar, click **"Run Database Migrations on Azure SQL"**
4. Click **"Run workflow"** button (top right)
5. Select **main** branch
6. Click **"Run workflow"**

This will:
- Create database tables for UserService
- Create database tables for ProductService
- Seed initial data (if configured)

**Wait for it to complete** (usually 2-3 minutes)

---

## ✅ Step 5: Test Deploy Workflow

### Option A: Manual Trigger
1. Go to **Actions** tab
2. Click **"Build and Deploy to Azure Container Apps"**
3. Click **"Run workflow"**
4. Select **main** branch
5. Click **"Run workflow"**

### Option B: Automatic Trigger (Recommended)
Make a small change to trigger the workflow:

```powershell
# Make a small change to a backend file
# For example, add a comment to UserService Program.cs
# Then commit and push:
git add Backend/UserService/Program.cs
git commit -m "Trigger CI/CD workflow"
git push origin main
```

The workflow will automatically:
1. Build Docker images
2. Push to Azure Container Registry
3. Update Container Apps with new images
4. Display service URLs

---

## 📊 Monitoring Workflows

### View Workflow Runs
- Go to **Actions** tab in GitHub
- Click on any workflow to see:
  - Status (✅ Success, ⏳ Running, ❌ Failed)
  - Logs for each step
  - Execution time

### Check Workflow Status
- Green checkmark ✅ = Success
- Yellow circle ⏳ = In progress
- Red X ❌ = Failed (check logs)

---

## 🔧 Troubleshooting

### Workflow Fails with "Authentication Failed"
- Check that `AZURE_CREDENTIALS` secret is correctly formatted (entire JSON)
- Verify service principal has Contributor role on resource group

### Migration Fails
- Check that `SQL_PASSWORD` secret is correct
- Verify SQL Server firewall allows GitHub Actions IPs (or use AllowAzureServices rule)
- Check connection string format

### Build Fails
- Check Dockerfile syntax
- Verify all dependencies are in .csproj files
- Check build logs for specific errors

### Container App Update Fails
- Verify Container Apps exist and are running
- Check that ACR images were pushed successfully
- Verify resource group name matches

---

## 📋 Workflow Summary

| Workflow | Trigger | What It Does |
|----------|---------|--------------|
| **deploy-azure-container-apps.yml** | Push to main (Backend changes) | Builds Docker images → Pushes to ACR → Updates Container Apps |
| **migrate-azure-database.yml** | Manual or migration file changes | Runs EF migrations on Azure SQL Database |
| **deploy-azure-angular.yml** | Push to main (Frontend changes) | Builds Angular app → Deploys to Static Web Apps |

---

## 🎯 Next Steps After Setup

1. ✅ Run migration workflow (Step 4) - **Do this first!**
2. ✅ Test deploy workflow (Step 5)
3. ✅ Create Static Web App (if not done)
4. ✅ Deploy Angular app using workflow
5. ✅ Test end-to-end functionality

---

**Need Help?** Check the workflow logs in GitHub Actions tab for detailed error messages.

