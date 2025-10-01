import { ISelectedChartData } from "../interface";

export interface IModalProps {
  isOpen: boolean;
  setIsOpen: Function;
  selectedChartData: ISelectedChartData;
  rowData: any[];
  setRowData: React.Dispatch<React.SetStateAction<any[]>>;
  colDef: any[];
  setColDef: React.Dispatch<React.SetStateAction<any[]>>;
}
