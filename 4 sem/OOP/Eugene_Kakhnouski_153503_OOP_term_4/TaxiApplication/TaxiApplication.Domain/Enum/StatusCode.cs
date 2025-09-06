namespace TaxiApplication.Domain.Enum;

public enum StatusCode
{
	/// <summary>
	/// User StatusCode 1**-> 
	/// 0-1*   -> 0 -> UserNotFound
	///			  1 -> UserAlreadyExist
	/// 0-1**  ->
	///  
	/// </summary>
	UserNotFound = 100,
	UserAlredyExist = 110,
	
	/// <summary>
	/// Authorizations errors 2**
	/// 0-1*	-> 0 -> WrongPassword
	///			-> 1 -> LoginAlredyExist
	/// </summary>
	WrongPassword = 200,
	LoginAlredyExist = 210,

	AllError = 404,
	OK = 777,

}
