import React from "react";
import SplitPane from "react-split-pane";

const SplitPaneVerticalCollapsable = ({ children, collapsed = false }) => {
  return (
    <div>
      <SplitPane
        split="vertical"
        minSize={350} //left pane drag length
        maxSize={-1450} //right pane drag length
        defaultSize={collapsed ? 50 : "30%"}
        size={collapsed ? 70 : "18.5%"} //opening default size
        allowResize={!collapsed}        
      >
        {children}
      </SplitPane>
    </div>
  );
};
export default SplitPaneVerticalCollapsable;

