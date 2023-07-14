# expenses-report-backend

## Architecture definition

More details:
[https://excalidraw.com/#json=ko9pFkoCun5zvubG7i_eY,RAyrmB8QKFn9Z2iUGl2hrw](https://excalidraw.com/#json=ko9pFkoCun5zvubG7i_eY,RAyrmB8QKFn9Z2iUGl2hrw)

![Initial architecture diagram](image.png)


## How to run locally (Development)

! You will need docker and docker-compose on your local machine !

on the root folder run:

``` docker compose -f Infra/docker-compose.yml up ```

Then hit the [http://localhost/swagger/index.html](http://localhost/swagger/index.html) location to see the swagger

![Swagger](image-1.png)

The routes are protected, don't forget to login and place the authorization header before hitting the endpoints

> User: admin@expense.report
>
> Pass: 123123123

! I couldn't figure out how to run the migrations inside the container yet, so please run the migration outside the container before using it (point the connection string to the mysql-db container)
dotnet ef database update

After the migrations were runned, then you can use the the docker compose for hosting the application.

## How to deploy (Production)

Build and deploy the image to ACR

! make sure you have the .env file with all production credentials !

```shell
docker build --tag acc-org-svc:1.3 .
docker tag acc-org-svc:1.4 expensesreportaccountorganizationservice.azurecr.io/acc-org-svc:1.4
```

> If you are using ARM like processor, you will need to build the image passing the --platform=linux/amd64 flag, so it can be used in the AKS pods

Log in ACR

```shell
docker login expensesreportaccountorganizationservice.azurecr.io
username: <AppId>
password: <Password>
```

Push the new image tag
``` docker push expensesreportaccountorganizationservice.azurecr.io/acc-org-svc:1.4 ```

Then in the azure bash shell, double check if you have access to pull the image
``` az aks check-acr --name aks-expenses --resource-group expenses-report-project --acr expensesreportaccountorganizationservice.azurecr.io ```

And change the tag of the image in the deployment.yml and deploy AKS by running
```shell
kubectl apply -f deployment.yml
kubectl apply -f service.yaml
kubectl get service expenses-project-service --watch
```

some other useful commands are
``` kubectl <get, delete> <pods, nodes, services, deployments> ```

Once your deployment is done, just copy the external IP and use that for calling the API

TODO: automate this process using github CI/CD and also Terraform