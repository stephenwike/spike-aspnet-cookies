# Spike - Dotnet ASP.net Cookies

This project was a quick spike to better understand the usage of cookies in ASP.net.

## Running the project.

Run the project in either IIS Express or run the project directly.  Enter any value into the form field and click 'Submit'.  The value next to the button will update with the value you just submitted.  This is all done using a cookie.

## Explaination of how the cookie is used.

### Calling the form submit API from the Home Controller

 When you click on the "Submit" button, a Form API is called with the FormCollection as a parameter.  This

```
[HttpPost]
public IActionResult Form(IFormCollection formCollection)
{
    CookieOptions options = new CookieOptions()
    {
        Expires = DateTime.Now.AddMinutes(2),
        HttpOnly = true,
    };
    Response.Cookies.Append("name", formCollection["txtCookie"], options);
    return RedirectToAction("Index", "Home");
}
```

This creates the cookie and sets its options including a two minute expiration and allowing only http access.  Then it redirects to the Index API.

### The Index Api from the Home Controller

The Index Api fetches the cookie created in the previous step if it exists.  Then adds the content of the cookie to a message property used by the view.

```
public IActionResult Index()
{
    if (Request.Cookies["name"] != null)
    {
        ViewBag.message = Request.Cookies["name"];
    }
    else
    {
        ViewBag.message = "Not available";
    }
    return View();
}
```

### The Home View

Inside the view, the message propery is displayed as defined in the step above.

```
<form method="post" asp-controller="Home" asp-action="Form">
    <input placeholder="Enter value for cookie" name="txtCookie"/><br />
    <label>@ViewBag.message</label>
    <button type="submit">Submit</button>
</form>
```

## Check the cookie in the browser

1) Navitage to chrome and click the three vertical dots on the top right corner.
2) Click "Settings"
3) Under "Privacy and Settings" click "Cookies and other site data".
4) Click "See all cookies and site data".
5) In "Search Cookies" field, type "localhost"
6) Expand the cookie details and then expand the cookie named "name".
7) Underneath, you should see "Content" with the content created (if within two minutes of when the "Submit" button was last pushed) and the "Accessible to script" section set to "No (HttpOnly)".