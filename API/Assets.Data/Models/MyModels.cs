using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Data.Models;
public class AssetRegion
{
    public int id { get; set; }
    public string name { get; set; }
    public int ParentId { get; set; }
}
public class AssetBoard //Cars or electronics
{
    public int id { get; set; }
    public string Name { get; set; } = "";
    public int RegionId { get; set; }
}
public class BoardUser
{
    public int id { get; set; }
    public int UserId { get; set; }
    public int UserTypeId { get; set; }
    public int BoardId { get; set; }
    public int VoteCount { get; set; }
}
public class AssetType
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
public class Manufacturer
{
    public int id { get; set; }
    public string name { get; set; }
    public int AssetTypeId { get; set; }
}
public class Asset
{
    public int id { get; set; }
    public string name { get; set; }
    public int AssetTypeId { get; set; }
    public int CategoryId { get; set; }
    public string Model { get; set; }
    public int ManufactureYear { get; set; }
    public int ManufacturerId { get; set; }
    public string SerialNo { get; set; }
    public string NumberPlate { get; set; }
    public string Description { get; set; }
    public int RegionId { get; set; }
    public int LocationId { get; set; }
}

public class AssetRequest
{
    public string id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    public int SerialNo { get; set; }
    public DateTime RequestDate { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public int RegionId { get; set; }
    public string AssetId { get; set; }
    public string RequestDetail { get; set; } = "";
    public string Notes { get; set; } = "";
    public int DepartmentId { get; set; }

}
public class ManagerResponse
{
    public string id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    public int SerialNo { get; set; }
    public int ManagerId { get; set; }
    public string RequestId { get; set; }
    public DateTime ResponseDate { get; set; } = DateTime.Now;
    public int ResponseTypeId { get; set; }
    public string Notes { get; set; } = "";

}
public class BoardResponse
{
    public string id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    public int SerialNo { get; set; }
    public DateTime ResponseDate { get; set; } = DateTime.Now;
    public string RequestId { get; set; } = "";
    public int BoardUserId { get; set; }
    public int ResponseTypeId { get; set; }
    public decimal VoteCount { get; set; }
    public string Notes { get; set; } = "";
}
public class FinalApproval
{
    public string id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    public int SerialNo { get; set; }
    public int UserId { get; set; }
    public DateTime ApprovalDate { get; set; }
    public string RequestId { get; set; }
    public int StatusId { get; set; }
    public string Notes { get; set; } = "";
}
public class AssetDelivery
{
    public string id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    public DateTime DeliveryDate { get; set; }
    public string ApprovalId { get; set; } = "";
    public string DocNo { get; set; } = "";
    public int UserId { get; set; }
    public string Notes { get; set; } = "";
}
public class AssetReturn
{
    public string id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    public DateTime ReturnDate { get; set; }
    public int AssetId { get; set; }
    public string DocNo { get; set; } = "";
    public int ReceivedBy { get; set; }
}