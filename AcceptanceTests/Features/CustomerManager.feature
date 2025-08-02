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
	
Scenario: An existing customer can be updated when the changes are valid
	Given an existing customer
	When the changes on the customer are valid
	Then the customer is updated successfully
	
Scenario: Existing customers can be displayed and deleted
	Given some customers exist
	When the customers list component is rendered
	Then the customers should appear in the table
	When clicking the delete button for customer A
	Then customer A should be removed from the table