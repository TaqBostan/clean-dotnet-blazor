Feature: Customer Manager

As a an operator I wish to be able to Create, Update, Delete customers and list all customers
	
@mytag
Scenario: A new customer can be created when is valid
	Given a new customer
	When the new customer is valid
	Then the customer is created successfully