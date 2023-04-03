using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace WebApplication2.Controllers;

public class UDPController : Controller
{
    // GET: /UDP/
    public string Index()
    {
        return "test";
    }

    // GET:/UDP/Test/
    public string Test()
    {
        return "test2";
    }
}
