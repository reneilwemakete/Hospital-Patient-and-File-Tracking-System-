using PRG281Assignment;

static void Check(bool condition, string name)
{
    if (!condition) throw new Exception("FAILED: " + name);
    Console.WriteLine("PASS: " + name);
}

const string password = "StrongDemoPassword123!";
var accounts = new AccountService("admin", password);
Check(accounts.Authenticate("admin", password)?.Access == AccessLevel.FullAccess,
    "Configured administrator can sign in");
Check(accounts.Authenticate("admin", "wrong-password") is null,
    "Incorrect password rejected");
var user = accounts.Register("nurse", password);
Check(user.Access == AccessLevel.ReadOnly, "Public registration cannot grant full access");
Check(accounts.Authenticate("NURSE", password) == user,
    "Registered user can sign in, ignoring username case");
Check(accounts.Authenticate("nurse", "bad-password") is null,
    "Wrong password for registered user rejected");
try { accounts.Register("NURSE", password); throw new Exception("Duplicate username allowed"); }
catch (InvalidOperationException) { Console.WriteLine("PASS: Duplicate username rejected"); }
try { accounts.Register("shortpassword", "short"); throw new Exception("Weak password allowed"); }
catch (ArgumentException) { Console.WriteLine("PASS: Weak password rejected"); }
try { _ = new AccountService("admin", null); throw new Exception("Partial admin configuration allowed"); }
catch (ArgumentException) { Console.WriteLine("PASS: Partial admin configuration rejected"); }
Console.WriteLine("All account regression checks passed.");
