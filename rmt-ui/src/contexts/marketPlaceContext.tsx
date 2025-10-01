import React, {
  Dispatch,
  SetStateAction,
  createContext,
  useState,
} from "react";

export interface IMarketPlaceContext {
  myFilter1: any;
  setMyFilter1: Dispatch<SetStateAction<any>>;
}
const initialState: IMarketPlaceContext = {
  myFilter1: {
    pagination: 1,
    limit: 3,
    buFiltervalue: [],
    industryFiltervalue: [],
    subIndustryFiltervalue: [],
    locationFiltervalue: [],
    isAllocatedFiltervalue: [],
    startDateFiltervalue: [],
    endDateFiltervalue: [],
  },
  setMyFilter1: (d: any) => {},
};
export const MarketPlaceContext =
  createContext<IMarketPlaceContext>(initialState);

export const MarketPlaceState: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [myFilter1, setMyFilter1] = useState(initialState.myFilter1);

  return (
    <MarketPlaceContext.Provider
      value={{
        myFilter1,
        setMyFilter1,
      }}
    >
      {children}
    </MarketPlaceContext.Provider>
  );
};
