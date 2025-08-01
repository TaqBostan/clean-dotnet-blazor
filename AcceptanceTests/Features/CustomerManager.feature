Feature: Customer Manager

As a an operator I wish to be able to Create, Update, Delete customers and list all customers
	
Scenario: A new customer can be created when is valid
	Given a new customer
	When the new customer is valid
	Then the customer is created successfully
	
Scenario: A new customer can not be created when is not valid
	Given a new customer
	When the new customer is not valid
	Then an error is displayed