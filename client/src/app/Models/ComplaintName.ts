import { Category } from "./Category";

export interface ComplaintName {
  id?: number; // Optional ID, assuming this is auto-generated
  name: string;
  categoryId: number;
  category?: Category; // Optional if you need to include category details
}
