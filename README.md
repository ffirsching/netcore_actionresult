# netcore_actionresult project

This is a sample project for my dev.to article;

A few things to consider:

- This is a mvp. For a production api there are a lot more things to consider!
- You should refrain from returning your domain entities directly through the api
- I omitted the service test since it's merely a facade passing through to the entity framework. You may want to test these still though.

## Get started:

- open a terminal in the src/ActionResultExample folder
- execute `dotnet run`
- api should now be listening on https://localhost:5001
- to try out the api you can use the swagger interface on https://localhost:5001/swagger/index.html

## Run tests:

- open a terminal in the src/tests/UnitTests folder
- execute `dotnet test`
