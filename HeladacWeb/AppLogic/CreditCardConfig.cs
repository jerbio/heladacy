using System.Collections.Generic;

public class CreditCardConfig {
    public CreditCardType creditCardType {get;set;}
    public ICollection<string> prefixes {get;set;}
    public int digitCount {get;set;}
    public string country { get; set; } = "us";
}