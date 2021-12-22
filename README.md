# Parkeringshuset
## Protection rules we used: 
  1. Wait timer set to 60 minutes from development to staging in order for us to be able to catch potential mistakes.
  2. Two external reviewers between staging from development.
  3. We require a pull request to merge into development.
  4. We require a review when merging into development.
  5. We require status check to pass for testDev before merging.

## Issues we have had:
  User could not erase registration number when started to type in the console.
   When a user started to type a reg number and made a mistake they could not go back by using backspace.
  This issue was a bugg and is now resolved.
  Issues is used so every developer in the project and users can see what issues exist and what issues that has been resolved.

## A Github component we havent learned about in class:
  Watch is a way to keep track of a repo's activity. The options of what to keep track of is as follows:
  Participating or @mentions (receive notifications when you are participating or when you are mentioned.)
  Custom (In addition to your own participation and when you are mentioned, you can choose between these activities: issues, pull requests, releases, discussions, security   alerts.)

  Github recommend us to review our subscriptions and watched repositories often.
   When a inbox has to many notifications to manage we should concider if we have oversubscribed and there is a way to change the types of notifications we recive.

  For example we can disable the setting that automatically makes us subscribe whenever we join a repository or team.
   It is easy to forget about repositories we have choosed to watch, to check your subscriptions you can click the Watch tab in the upper right hand corner of Github 
   and this is also where you can change the settings.

  All activity (receive notifications about all activity on the repo.)
  Ignore (You do not get notified.)

## Two more types of testing.
In this project we used two types of tests other then Unit tests and Integration tests.

The first test we used was beta/acceptance testing. Benny asked his mother to use the program
 and try to check vehicles in and out of the system and use different types of parking spots and also try the simulated payment.
 She found the bugg not beeing able to erase a input using backspace by misstake when she typed the wrong registration number.

The second test we used was regression testing after we implemented AdminController methods. 
 We used it to make sure the old tests and the system was working after implementing the admin features.
