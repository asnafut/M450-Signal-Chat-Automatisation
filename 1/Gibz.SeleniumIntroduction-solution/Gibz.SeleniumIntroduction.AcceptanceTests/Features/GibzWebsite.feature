Feature: GibzWebsite

Scenario: Check Berufsverantwortliche from Informatiker/in EFZ
	Given I open the GIBZ website
	And I click on Grundbildung
	And I click on Unsere Berufe
	When I click on Informatiker/in EFZ
	Then the first Berufsverantwortlicher is Peter Gisler
	And the second Berufsverantwortlicher is Tobias Schmid

Scenario: Check number of Berufe
	Given I open the GIBZ website
	And I click on Grundbildung
	When I click on Unsere Berufe
	Then <NumberOfBerufe> Berufe starting with <StartingKey> should be displayed
Examples: 
| NumberOfBerufe | StartingKey |
| 5              | A           |
| 1              | C           |
| 4              | E           |
| 3              | F           |
| 1              | H           |
| 2              | I           |
| 3              | K           |
| 2              | M           |
| 1              | P           |
| 1              | R           |
| 2              | S           |
| 3              | Z           |