Project Setup and API Documentation

## Setup Steps

### Prerequisites
- Azure **CLI**
- **kubectl**
- **Docker**

### Access the Cluster
az login
az aks get-credentials --resource-group <ResourceGroupName> --name <ClusterName> --overwrite-existing

### Infrastructure Deployment
kubectl apply -f mongodb-deployment.yaml
kubectl apply -f units-api-deployment.yaml
kubectl apply -f units-api-service.yaml
kubectl apply -f api-ingress.yaml

### Scaling Configuration
kubectl autoscale deployment units-api --cpu-percent=50 --min=1 --max=10

## API Endpoints (Public IP: 104.44.176.111)
- `/swagger/index.html` (GET) – Swagger UI
- `/weatherforecast` (GET) – Load testing & HPA validation
- `/health` (GET) – Health check
- `/` (GET) – Root endpoint

## System Monitoring
kubectl get hpa -w
kubectl get pods -o wide
kubectl logs -l app=units-api

## Architecture Summary
- **AKS** – Container orchestration
- **NGINX Ingress** – External access & routing
- **MongoDB StatefulSet** – Stable storage
- **.NET Minimal API** – Backend service