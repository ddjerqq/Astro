namespace Domain.Entities;

public class ItemType
{
    ItemTypeId Id { get; set; } 
    string Name { get; set; }
    string? Description { get; set; } 
    float QualityMin { get; set; } // the minimum quality this item appears in
    float QualityMax { get; set; } // the maximum quality this item can have
    bool StatTrackAvailable { get; set; }// whether or not stat track is available
    string ImageUrl { get; set; }
}