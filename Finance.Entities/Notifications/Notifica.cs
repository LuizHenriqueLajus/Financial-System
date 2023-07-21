using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Finance.Entities.Notifications;

public class Notifica
{
    public Notifica()
    {
        notifications = new List<Notifica>();
    }

    [NotMapped]
    public string NameProperty { get; set; }

    [NotMapped]
    public string message { get; set; }

    [NotMapped]
    public List<Notifica> notifications { get; set; }

    public bool ValidatePropertyString(string valor, string nameProperty)
    {
        if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nameProperty))
        {
            notifications.Add(new Notifica
            {
                message = "Required field",
                NameProperty = nameProperty
            });

            return false;
        }

        return true;
    }

    public bool ValidatePropertyInt(int valor, string nameProperty)
    {

        if (valor < 1 || string.IsNullOrWhiteSpace(nameProperty))
        {
            notifications.Add(new Notifica
            {
                message = "Required field",
                NameProperty = "nameProperty"
            });

            return false;
        }

        return true;

    }

}
