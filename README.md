I made this app because all of the basketball shot tracking app did not meet my needs.

Lessons Learned:

Builder in program.cs can inject dbcontext using connectionstring. You do not need Onconfiguring Method of Context

Async can start multiple methods at once. Await is what makes the method wait for the thread to be finished before continuing to the next step.

Have UseAuthntication to avoid 500 error when oidc redirect url is called

You don't need a client registration when signing up users, you only need the registered app of the current app

Client only need to register if it is a daemon application (console), or backend service client

include partial validation script with error class text if you want jquery clientside validation

viewModel should be used for client validation

make sure to not divide by 0

use Data table for pagination

int divide by int will result = 0 if there are decimals. You must have type as decimal and cast at least one of the int to decimal to get the correct answer

App Service has a free tier

uaw hiddenFor for fields that are needed but not in the form in razor pages

Get better at Linq, or use include method if join is needed

try to use IEnumerable to join tables.

To Do List:

Have percentage by month going back by a year

Have filters to see specific sessions

