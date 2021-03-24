## Description
An ASP.NET MVC webapp that uses API from bitrix24. Authentication with usage of EasyOauth2.

### <a name="installguide"></a> Installation guide
Clone the repo in with 
  + HTTPS: <code>git clone https://github.com/tuannamtruong/bitrix24.git</code> or
  + SSH: <code>git clone git@github.com:tuannamtruong/bitrix24.git</code>

Go to the folder <code>bitrix24\bitrix24\bitrix24</code> 

Create a text file name <code>appid.txt</code> 

Paste the <code>appid</code> to this text file.


## Run guide
Go to <code>bitrix24\bitrix24</code> 

Open <code> bitrix24.sln </code>  

Run the project (F5).

## Deploy guide
Finish the [installation guide](#installguide), so that the <code>appid.txt</code> is in the project folder.

Go to the project folder at <code>bitrix24\bitrix24\bitrix24</code>.

Deploy with command line or powershell. <code> dotnet publish bitrix24.csproj -c release -r (RUNTIME_IDENTIFIER) --self-contained </code>

RUNTIME_IDENTIFIER value can be one of the following ([full details](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog))
  + win10-x64
  + linux-x64
  + osx-x64

The result will be stored in the this folder: <code> bitrix24\bitrix24\bitrix24\bin\release\netcoreapp3.1\(RUNTIME_IDENTIFIER)\\**publish** </code>. 
