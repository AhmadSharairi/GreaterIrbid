export interface CitizenSatisfaction {
  id: number;
  fullName: string;
  phoneNumber: string;
  email?: string; // Optional field
  rating: number;
  comments?: string; // Optional field
}
