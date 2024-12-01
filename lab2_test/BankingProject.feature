Feature: Customer Management
  As a bank manager
  I want to delete a customer
  So that the customer is no longer in the list

  Scenario: Delete a customer from the list
    Given I am on the Banking Project homepage
    When I click on the Bank Manager Login button
    And I click on the Customers button
    And I delete the first customer in the list
    Then the customer should be removed from the list
