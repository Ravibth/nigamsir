import React from "react";
import { Oval } from "react-loader-spinner";
import "./loader.css";

const Loader = (props: any) => {
  return (
    <div className={props?.small ? "small_loader" : "loader"}>
      <Oval
        height={props?.small ? 40 : 80}
        width={props?.small ? 40 : 80}
        color="#4fa94d"
        wrapperStyle={{}}
        wrapperClass=""
        visible={true}
        ariaLabel="oval-loading"
        secondaryColor="#4fa94d"
        strokeWidth={2}
        strokeWidthSecondary={2}
      />
    </div>
  );
};
export default Loader;
