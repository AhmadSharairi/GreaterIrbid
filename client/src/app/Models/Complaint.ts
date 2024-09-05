export interface Complaint {
  id?: number;
  name?: string;
  mobile?: string;
  email?: string;
  nationality?: string;
  complaintAgainstName?: string;
  region?: string;
  nearestComplaintLocation?: string;
  buildingNumber?: string;
  complaintPrivacy?: string;
  complaintDate: Date;
  complaintDetails: string;
  imageUrlComplaint?: string;
  shareLocationComplaint?: string;
  complaintNameId: number;
}
