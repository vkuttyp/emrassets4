using System.Net;

namespace Assets.Data.Models;
public class RestResponse<T>
{
    public T? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Unused;
    public Exception? Exception { get; set; }
}
public class ArpUser
{
    public int id { get; set; }
    public int ManagerId { get; set; }
    public string? DepartmentId { get; set; }
    public string? DepCode2 { get; set; }
    public int DepManager2 { get; set; }
    public int GroupId { get; set; }
    public int UserTypeId { get; set; }
    public string? DepartmentName { get; set; }
    public string? name { get; set; }
    public string? IdNo { get; set; }
    public string? Title { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool IsManager { get; set; }
    public bool Collapsed { get; set; }
    public ArpUser? Manager { get; set; }
    public ArpDepartment? Department { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
    public List<ArpUser>? Subordinates { get; set; }
    public List<CarDeliveryDetail> CarDeliveries { get; set; }
    public List<CarRequest> CarRequests { get; set; }
    public BoardMember? CarBoardMember { get; set; }
    public string Token { get; set; }
}
public class ArpDepartment
{
    public string? id { get; set; }
    public string? name { get; set; }
    public int ManagerId { get; set; }
}

public class LoginData
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? MachineName { get; set; }
}
public class ADUserInfo
{
    public string IdNo { get; set; } = "";
    public string FullName { get; set; } = "";
    public string MobileNo { get; set; } = "";
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
}

public class MyListItem
{
    public MyListItem(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
    public MyListItem()
    {
        
    }
    public int id { get; set; }
    public string? name { get; set; }
}
public record Problem(int Status, string Type, string Title, string Detail);