Feature: Banking Project

  Scenario: Deleting a customer from the system
    Given I open the Banking Project webpage
    When I click on the "Bank Manager Login" button
    And I click on the "Customers" tab
    And I delete the first customer from the list
    Then I should not see the deleted customer in the list
