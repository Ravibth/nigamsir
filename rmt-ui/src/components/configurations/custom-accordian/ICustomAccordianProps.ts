export interface ICustomPropsInterface {
  children?: React.ReactNode;
  title: string | null;
  configNote: string | null;
  isOpen: boolean | null;
  isEditable: boolean | null;
  hideEdit?: boolean | false;
  handleAccordianOpenClick: Function | any;
  handleAccordianCloseClick: Function | any;
  handleEditClick: Function | any;
  handleCancelClick: Function | any;
  handleSaveClick?: Function | any;
}
