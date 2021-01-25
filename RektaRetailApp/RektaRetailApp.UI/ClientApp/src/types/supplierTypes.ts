export interface ICreateSupplier {
  name: string
  phoneNumber: string
  description: string
}

export type Routes = {
  path: string
  name: string
  icon: string
  component: React.FC<any>
  subMenu: SubRoute[]
}[]

export type SubRoute = {
  path: string
  name: string
  icon: string
  component: React.FC<any>
}

export type DashboardProps = {
  fixedHeightPaper: string
  classes: Record<
      | 'root'
      | 'content'
      | 'toolbar'
      | 'toolbarIcon'
      | 'appBar'
      | 'appBarShift'
      | 'menuButton'
      | 'menuButtonHidden'
      | 'title'
      | 'drawerPaper'
      | 'drawerPaperClose'
      | 'appBarSpacer'
      | 'container'
      | 'paper'
      | 'fixedHeight',
      string
      >
}

export interface CategoryInterface {
  categoryName: string;
  categoryId: number;
  categoryDescription: string;
}
export interface CreateInventoryProp {
  name: string;
  description: string;
  batchNumber: string;
  categoryName: string;
  productQuantity: number;
  supplyDate: Date;
}

export interface ProductSuppliedType {
  id: number;
  name: string;
  quantity: number;
}
export interface SupplierDetailType {
  name: string;
  phoneNumber: string;
  description: string;
  supplierId: number;
  suppliedProducts: ProductSuppliedType[];
}

export interface SupplierResponseType {
  data: SupplierDetailType;
  errors: string[];
  currentResponseStatus: string;
}