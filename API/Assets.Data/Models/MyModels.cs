namespace Assets.Data.Models;


public class BoardMember
{
    public int id { get; set; }
    public int ArpId { get; set; }
    public int MemberTypeId { get; set; }
    public BoardMemberType BoardMemberType { get; set; }
}
public class BoardMemberType
{
    public int id { get; set; }
    public string name { get; set; }
    public int VoteCount { get; set; }
}
public class CarCategory
{
    public int id { get; set; }
    public string name { get; set; }
}
public class CarType
{
    public int id { get; set; }
    public string name { get; set; }
}
public class Department
{
    public int id { get; set; }
    public string name { get; set; }
    public int TypeId { get; set; }
    public int RegionId { get; set; }
}

public class Car
{
    public int id { get; set; }
    public string name { get; set; }
    public int CarTypeId { get; set; }
    public int CategoryId { get; set; }
    public string Model { get; set; }
    public int ManufactureYear { get; set; }
    public int ManufacturerId { get; set; }
    public string SerialNo { get; set; }
    public string NumberPlate { get; set; }
    public string Description { get; set; }
    public int RegionId { get; set; }
    public int LocationId { get; set; }
    public CarCategory Category { get; set; }
    public CarType CarType { get; set; }
}

public class CarRequest
{
    public string? id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    public int SerialNo { get; set; }
    public DateTime? RequestDate { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public int RegionId { get; set; }
    public string? RequestDetail { get; set; } = "";
    public string? Notes { get; set; } = "";
    public int DepartmentId { get; set; }
    public int CarId { get; set; }
    public CarManagerResponse? CarManagerResponse { get; set; }=new CarManagerResponse();
    public ArpUser? Beneficiary { get; set; }
    
}
public class CarManagerResponse
{
    public string id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    public int SerialNo { get; set; }
    public int ManagerId { get; set; }
    public string RequestId { get; set; } = "";
    public DateTime ResponseDate { get; set; } = DateTime.Now;
    public int ResponseTypeId { get; set; }
    public string Notes { get; set; } = "";
    public MyListItem? ManagerResponseType { get; set; } = new MyListItem();
    public CarRequest? CarRequest { get; set; }
    public ArpUser? Manager { get; set; }
    public List<CarVotingDetail>? Votings { get; set; } = new();
    public CarVotingDetail? CurrentUserVoting { get; set; }
    public int TotalVotesCount { get; set; }
    public int TotalVotesObtained=> Votings?.Sum(a=>a.VoteCount) ?? 0;
    public CarVotingFinalDecision? CarVotingFinalDecision { get; set; }
    public bool HasFinalDecision => CarVotingFinalDecision != null;
}
public class CarVotingDetail
{
    public string id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    public int SerialNo { get; set; }
    public DateTime VotingDate { get; set; } = DateTime.Now;
    public string ManagerResponseId { get; set; } = "";
    public int MemberId { get; set; }
    public int ResponseTypeId { get; set; }
    public int VoteCount { get; set; }
    public string Notes { get; set; } = "";
    public MyListItem? ResponseType { get; set; }=new();
}
public class CarVotingFinalDecision
{
    public string id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    public string ManagerResponseId { get; set; } = "";
    public int CarId { get; set; }
    public int SerialNo { get; set; }
    public DateTime DecisionDate { get; set; }=DateTime.Now;
    public int UserId { get; set; }
    public int ResponseTypeId { get; set; }
    public string Notes { get; set; } = "";
    public int BeneficiaryId { get; set; }
    public MyListItem? ResponseType { get; set; } = new();
    public Car? Car { get; set; }
    public ArpUser? Beneficiary { get; set; }
}
public class CarDeliveryDetail
{
    public string id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    public int DeliveredTo { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string ApprovalId { get; set; } = "";
    public string DocNo { get; set; } = "";
    public int UserId { get; set; }
    public string Notes { get; set; } = "";
    public int CarId { get; set; }
    public Car Car { get; set; }
}