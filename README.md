# SparkPost C# Webhooks Sample

This project contains a .NET Core service that consumes and processes [SparkPost webhooks](https://developers.sparkpost.com/api/webhooks.html).


[![Deploy to Azure](https://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/?repository=https://github.com/SparkPost/azure-csharp-webhook-sample)

## Prerequisites

* A shiny new SparkPost Account - [sign up](https://app.sparkpost.com/)
* .NET Core Runtime 2.0 ([Download](https://dot.net/core))
* [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio 2017](https://www.visualstudio.com/)

## Installation

```
git clone https://github.com/SparkPost/azure-csharp-webhook-sample.git
cd SparkPostWebhooksSample
dotnet restore
dotnet ef database update
dotnet run
```

The service is now listening on port 5000. It expects to receive `POST` requests to the webhook endpoint [http://localhost:5000/api/webhook](http://localhost:5000/api/webhook) containing valid SparkPost webhook batches in JSON format.

## Usage

You can use [ngrok](https://ngrok.com/) to connect your local service to your SparkPost account nand send a test batch:
1. Start ngrok: `ngrok http 5000`. Ngrok will create a tunnel and give you a public URL for your service.
1. Create a new [Webhook](https://app.sparkpost.com/account/webhooks) on your SparkPost Account that points to your ngrok tunnel URL.
1. Use the [webhook test facility](https://app.sparkpost.com/account/webhooks) in SparkPost to send a test batch to your service.

When you send email through your SparkPost account, your webhook service will periodically receive batches of real events.

**Note:** Remember to shut your service down, remove the tunnel and delete the webhook from your SparkPost account when you're finished.

## Reference Material

* [SparkPost Webhooks Guide](https://www.sparkpost.com/blog/webhooks-beyond-the-basics/)
* [SparkPost API reference on webhooks](https://developers.sparkpost.com/api/webhooks.html)
