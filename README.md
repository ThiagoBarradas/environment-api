# Environment API

A simple API that returns environment variables to use in talks, demos, etc;

## Running with Docker

Command to run Environment API (port 8088)

```
docker run --name environment-api -p 8088:80 -d thiagobarradas/environment-api:latest
```

## How to use

Send a request to get environment variables:

`GET environment/{environmentVariables}`

You can send many values in `environmentVariables` separated by pipe like `VAR_X|VAR_Y|VAR_Z`;

When some variable not exists, your value in response will be `[not found]`

### Sample

Request: 

`GET environment/ASPNETCORE_ENVIRONMENT|OTHER_VAR`

Response:

```
{
  "ASPNETCORE_ENVIRONMENT": "Development",
  "OTHER_VAR": "[not found]"
}
```

## How can I contribute?

Please, refer to [CONTRIBUTING](.github/CONTRIBUTING.md)

## Found something strange or need a new feature?

Open a new Issue following our issue template [ISSUE TEMPLATE](.github/ISSUE_TEMPLATE.md)

## Did you like it? Please, make a donate :)

if you liked this project, please make a contribution and help to keep this and other initiatives, send me some Satochis.

BTC Wallet: `1G535x1rYdMo9CNdTGK3eG6XJddBHdaqfX`

![1G535x1rYdMo9CNdTGK3eG6XJddBHdaqfX](https://i.imgur.com/mN7ueoE.png)
