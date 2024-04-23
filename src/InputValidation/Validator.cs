namespace InputValidation;

public class Validator : IValidator
{
  public bool IsValidEmail(string email)
  {
        // TODO - replace the below exception with your own implementation

        bool isValidEmail = true;
        string failCase = "Email invalid: ";

        //Check if it contains "@"
        if (!email.Contains("@"))
        {
            isValidEmail = false;
            failCase = failCase + "   needs a @";
        }


        //Check if there are any letters before @
        int letterIndexOfEmail = email.IndexOf("@");
        if
            (letterIndexOfEmail == 0)
        {
            isValidEmail = false;
            failCase = failCase + "   needs text before @";
        }


        //Check if email ends with ".com"
        if (!email.EndsWith(".com"))
        {
            isValidEmail = false;
            failCase = failCase + "   needs .com at end of mail";
        }

        if (isValidEmail)
        {
            failCase = "mail is valid!";
        }

        Console.WriteLine(failCase);
        return isValidEmail;

  }
}
