using System.Net.Http.Json;
using System.Text.Json;

var x = new HttpClient();
int idmiz=0;
string arabaAdi="";
string port = "5183";
string ip = "http://10.46.121.26:";
var verim = new Verim();
Console.WriteLine("------------------Favori  arabalarımı tuttuğum servere bağlanılıyor------------------------");
Console.WriteLine("Şuanki favori arabalarım");
var ListOfCars = await x.GetFromJsonAsync<List<Car>>(ip+port+"/ListOfCars");
foreach (var k in ListOfCars)
{
    Console.WriteLine($"{ListOfCars.IndexOf(k)+1}. Araba {k.ArabaAdi} ");
}
Console.WriteLine("Araba listesini güncellemek için 1,Araba Modelini Değiştirmek için 2 çıkmak için 3 yazınız:");
int userInput = int.Parse(Console.ReadLine());
while (userInput != 3)
{
    if (userInput == 1)
    {
        ListOfCars = await x.GetFromJsonAsync<List<Car>>(ip + port + "/ListOfCars");
        Console.WriteLine("Güncellenmiş Araba Listesi:");

        foreach (var k in ListOfCars)
        {
            Console.WriteLine($"{ListOfCars.IndexOf(k) + 1}. Araba {k.ArabaAdi} ");
        }
    }
    if (userInput == 2)
    {
        Console.WriteLine("Güncellemek istediğiniz araba id'sini  giriniz:");
        idmiz = int.Parse(Console.ReadLine());

        Console.WriteLine("Yeni araba modelinizi giriniz:");
        arabaAdi = Console.ReadLine();
        verim.AArabaAdi = arabaAdi;
        verim.Id = idmiz;
        await x.PostAsJsonAsync<Verim>(ip + port + "/ListOfCars/arabaAdi", verim);

        Console.WriteLine("Değiştirilmiş Araba Listesi:");
        ListOfCars = await x.GetFromJsonAsync<List<Car>>(ip + port + "/ListOfCars");
        foreach (var k in ListOfCars)
        {
            Console.WriteLine($"{ListOfCars.IndexOf(k) + 1}. Araba {k.ArabaAdi} ");
        }

    }
    Console.WriteLine("Araba listesini güncellemek için 1,Araba Modelini Değiştirmek için 2 çıkmak için 3 yazınız:");
    userInput = int.Parse(Console.ReadLine());
}


public class Car
{
    public string ArabaAdi { get; set; }
    public Car() { }
    public Car(string ArabaAdi)
    {
        this.ArabaAdi = ArabaAdi;
    }

}
public class Verim
{
    public string AArabaAdi { get; set; }
    public int Id { get; set; }
}