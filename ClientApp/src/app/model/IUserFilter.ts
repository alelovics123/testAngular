export interface IUserFilter {
  id: number | null;
  nameOrPlaceFilter: string | null;
  fromDateOfBirth: Date | null;
  toDateOfBirth: Date | null;
  recommenderId: number | null;
}

