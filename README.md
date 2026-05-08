# Project Setup and API Documentation

## Setup Steps

### Prerequisites
Make sure the following tools are installed:

- Azure CLI  
- kubectl  
- Docker  

---

### Access the Cluster

```bash
az login
az aks get-credentials --resource-group <ResourceGroupName> --name <ClusterName> --overwrite-existing
```

---

### Infrastructure Deployment

Apply all Kubernetes manifests:

```bash
kubectl apply -f mongodb.yaml
kubectl apply -f api-deployment.yaml
kubectl apply -f service.yaml
kubectl apply -f ingress.yaml
```

---

### Scaling Configuration

Enable horizontal pod autoscaling:

```bash
kubectl autoscale deployment units-api --cpu-percent=70 --min=1 --max=5
```

---

## API Endpoints

**Public IP:** `104.44.176.111`

- `/swagger/index.html` (GET) – Swagger UI

- `/weatherforecast` (GET) – Used for load testing and HPA validation

- `/health` (GET) – Health check endpoint

- `/` (GET) – Root endpoint

---

### Posts API

- `/posts` (GET) – Retrieve all posts

- `/posts` (POST) – Create a new post

- `/posts/{id}` (GET) – Retrieve a post by ID

- `/posts/{id}` (PUT) – Update a post

- `/posts/{id}` (DELETE) – Delete a post
---

## System Monitoring

Useful commands to observe system behavior:

```bash
kubectl get hpa -w
kubectl get pods -o wide
kubectl logs -l app=units-api
```

---

## Architecture Summary

- **AKS (Azure Kubernetes Service)** – Container orchestration  
- **NGINX Ingress Controller** – External access and routing  
- **MongoDB StatefulSet** – Persistent and stable storage  
- **.NET Minimal API** – Backend service  
