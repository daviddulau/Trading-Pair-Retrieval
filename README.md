# Pair Price Retrieval
Retrieving trading pair closing prices and returning the aggregated values 

## Project PriceRetrieval
This is the main project containing the logic of the app in the following folder structure

-- Commands: implemented the get pairs command
-- Models: data structures to create an internal protocol
-- Providers: Api clients for the Bitfinex API and Bitstamp API
-- Services: There is the PriceRetrievalService
  
 
## Project PriceRetrieval.API
Defines the endpoints to connect to the world. The API is versioned.

## PriceRetrieval.UnitTests
Unit tests.

## Technologies
- [FluentValidation]
- [Scrutor] - Di decoration
- [xUnit]
- [FluentAssertions]

## Postman
Postman collections are under docs/Postman.
