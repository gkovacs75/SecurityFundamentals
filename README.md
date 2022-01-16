## ASP.NET Code Web Application using Razor
#### - Demonstrates basic security ideas from scratch
- Principal
- Identity
- Claims
- Security Context
- Cookie


## Authentication
- Verify that you are who you say you are
- Generate the Security Context
- Security Context
 - This contains info about you
 - In Cookie authentication, we generate a cookie
 - In Token authentication, we generate a token
- This is the process of logging in, providing the username and password
- So you have a cookie or a token that contains a security context
- When you go to various pages, you will carry the security context
- The web server
 - Verifies your credentials
 - Talks to a data store
 - Generates the security context
- Cookies arent shares to other domains
- Every call to the server will contain this cookie
 - The server will desearialize and decrypt the cookie and authenticate the user
 - Then we Authorize and see if the user has rights to the page or whatever



## Authorization
- Verifying that the Security_Context(your indentity) satisfies the access requirements
- Each page can have different requirements so it will look at the security context to see if you have these requirements
- a 403 will be returned if you dont have the rights to a page
- a 401 will be returned if you are not even authenticated (logged in)

### ASP.NET Core Security Context
- Claims Principal Object or simply Principal
 - Contains Username, email, etc. of the logged-in-user
 - The Principal IS the user
- Principal contains one or more Identities
 - e.g. Student card, Drivers license, Apartment Access Card.
 - Usually we have one identity
- An Identity can contain one or more Claims
- Claims are key/value pairs. e.g. Age: 40
 
## Authorization Architecture and Flow




