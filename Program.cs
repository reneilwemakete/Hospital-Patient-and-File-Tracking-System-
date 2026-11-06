namespace PRG281Assignment;
internal static class Program {
 static readonly List<Patient> Patients=new() {
  new Patient {Id="DEMO001",Name="Sample Patient One",LastUpdated=DateTime.Now},
  new Patient {Id="DEMO002",Name="Sample Patient Two",LastUpdated=DateTime.Now}
 };
 static void Main() {
  AccountService accounts;
  try { accounts=new AccountService(Environment.GetEnvironmentVariable("HOSPITAL_ADMIN_USER"),Environment.GetEnvironmentVariable("HOSPITAL_ADMIN_PASSWORD")); }
  catch(ArgumentException ex){Console.Error.WriteLine(ex.Message);return;}
  while(true){
   Console.WriteLine("\n=== HOSPITAL PATIENT AND FILE TRACKING ===\n1. Login\n2. Register read-only account\n3. Exit");
   Console.Write("Choose option: ");
   switch(Console.ReadLine()){
    case "1":
     Console.Write("Username: ");var username=Console.ReadLine()??"";
     Console.Write("Password: ");var password=ReadPassword();
     var user=accounts.Authenticate(username,password);
     if(user is null)Console.WriteLine("Invalid credentials.");else ShowUserMenu(user);
     break;
    case "2":
     try{
      Console.Write("Choose username: ");var name=Console.ReadLine()??"";
      Console.Write("Choose password (12+ characters): ");
      accounts.Register(name,ReadPassword());
      Console.WriteLine("Read-only account created. Please log in.");
     }catch(ArgumentException ex){Console.WriteLine(ex.Message);}
      catch(InvalidOperationException ex){Console.WriteLine(ex.Message);}
     break;
    case "3":return;
    default:Console.WriteLine("Choose 1, 2 or 3");break;
   }
  }
 }
 static string ReadPassword(){
  if(Console.IsInputRedirected)return Console.ReadLine()??"";
  var chars=new List<char>();
  while(true){
   var key=Console.ReadKey(true);
   if(key.Key==ConsoleKey.Enter){Console.WriteLine();break;}
   if(key.Key==ConsoleKey.Backspace){if(chars.Count>0)chars.RemoveAt(chars.Count-1);}
   else if(!char.IsControl(key.KeyChar))chars.Add(key.KeyChar);
  }
  return new string(chars.ToArray());
 }
 static void ShowUserMenu(User user){
  var full=new FullAccessActions(Patients);
  var readOnly=new ReadOnlyActions(Patients);
  while(true){
   user.DisplayMainMenu();Console.Write("Select option: ");
   var choice=Console.ReadLine();
   if(choice=="1"){readOnly.ViewPatients();continue;}
   if(user.Access==AccessLevel.FullAccess){
    switch(choice){
     case "2":full.AddPatientFile();break;
     case "3":full.EditPatientFile();break;
     case "4":full.DeletePatientFile();break;
     case "5":full.GenerateReport();break;
     case "6":return;
     default:Console.WriteLine("Invalid option.");break;
    }
   }else{
    switch(choice){
     case "2":readOnly.GenerateReport();break;
     case "3":return;
     default:Console.WriteLine("Invalid option.");break;
    }
   }
  }
 }
}