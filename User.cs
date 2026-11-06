using System.Security.Cryptography;
namespace PRG281Assignment;
public enum AccessLevel { ReadOnly = 1, FullAccess = 2 }
public sealed class User {
 public string Username { get; }
 public AccessLevel Access { get; }
 private readonly byte[] salt;
 private readonly byte[] hash;
 public User(string username,string password,AccessLevel access) {
  if(string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username required");
  if(string.IsNullOrWhiteSpace(password)||password.Length<12) throw new ArgumentException("Password must contain at least 12 characters");
  Username=username.Trim(); Access=access;
  salt=RandomNumberGenerator.GetBytes(16);
  hash=Rfc2898DeriveBytes.Pbkdf2(password,salt,210000,HashAlgorithmName.SHA256,32);
 }
 public bool VerifyPassword(string? password) {
  if(password is null)return false;
  return CryptographicOperations.FixedTimeEquals(hash,Rfc2898DeriveBytes.Pbkdf2(password,salt,210000,HashAlgorithmName.SHA256,32));
 }
 public void DisplayMainMenu() {
  Console.Clear(); Console.WriteLine($"=== MAIN MENU ({Username}) ===\nAccess Level: {Access}\n1. View Patients");
  if(Access==AccessLevel.FullAccess)Console.WriteLine("2. Add Patient\n3. Edit Patient\n4. Delete Patient\n5. Generate Report\n6. Logout");
  else Console.WriteLine("2. Generate Report\n3. Logout");
 }
}
public sealed class AccountService {
 private readonly List<User> users=new();
 public AccountService(string? adminUsername,string? adminPassword) {
  if(!string.IsNullOrWhiteSpace(adminUsername)&&!string.IsNullOrWhiteSpace(adminPassword))
   users.Add(new User(adminUsername,adminPassword,AccessLevel.FullAccess));
  else if(!string.IsNullOrWhiteSpace(adminUsername)||!string.IsNullOrWhiteSpace(adminPassword))
   throw new ArgumentException("Set both HOSPITAL_ADMIN_USER and HOSPITAL_ADMIN_PASSWORD");
 }
 public User Register(string username,string password) {
  if(string.IsNullOrWhiteSpace(username))throw new ArgumentException("Username required");
  if(users.Any(u=>string.Equals(u.Username,username.Trim(),StringComparison.OrdinalIgnoreCase)))
   throw new InvalidOperationException("Username already exists");
  var user=new User(username,password,AccessLevel.ReadOnly);users.Add(user);return user;
 }
 public User? Authenticate(string username,string password) {
  if(string.IsNullOrWhiteSpace(username)||password is null)return null;
  var user=users.FirstOrDefault(u=>string.Equals(u.Username,username.Trim(),StringComparison.OrdinalIgnoreCase));
  return user is not null&&user.VerifyPassword(password)?user:null;
 }
}