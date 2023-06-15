public class IdCard
{
    public string firstName;
    public string lastName;
    public string birthDate;
    public string cnp;
    public string citizenship;
    public string gender;

    public IdCard(string firstName, string lastName, string birthDate, string cnp, string citizenship, string gender)
    {
        this.lastName = lastName;
        this.firstName = firstName;
        this.birthDate = birthDate;
        this.cnp = cnp;
        this.gender = gender;
        this.citizenship = citizenship;
    }
}
