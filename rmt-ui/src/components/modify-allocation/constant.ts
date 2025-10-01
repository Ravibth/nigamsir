import { SxProps } from "@mui/material";
import * as GlobalConstant from "../../global/constant";

export const Btn : SxProps = {
    textTransform:"initial",
    color:GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
    
}

export const Btncancel : SxProps = {
    textTransform:"initial",
    paddingLeft:"200px",
    color:"grey",
    '&:hover': {
        color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor
    }
}
export const Textbox : SxProps = {
    width:"100px"
}

export const Title : SxProps = {
    fontSize:"20px",
    color:"grey"
}

export const AddmoreBtn : SxProps = {
    color: GlobalConstant.GT_DESIGN_PARAMETERS.GtPrimaryColor,
    textTransform:"initial"
}
