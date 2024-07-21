export interface GridRequestParamters {
  filter: string;
  sort: string;
  orderBy: string;
  orderEnum: 'asc' | 'desc';
  take: number;
  skip: number;
  total: number;
}
