# frederik.app.wpf

## Description
Short but unfinished demo application of a robot grabbing wafers from one loadport on the left, rotate the arm and pushing it into loadport 2.

## Architecture
The workflow model is the main class, which contains the logic of handling cassettes with wafers and the robot arm.
Tried to split everything into small classes and make it reusable as possible

## Considerations
 - No branches in GIT, I just merge everything in master, as I'm creator and reviewer
 - No worker is inserting the cassette with wafers, for simplicity now it's just there
 - I assume always 25 wafers per cassette, but can be edited in code
 - Exceptions are caught but not further handled
 - No stop button requested
 - Using flat hirachy in project

## Open points
- Adding missing events to notifiy view in models
- Adding missing usercontrol for robot arm
- testing everything
