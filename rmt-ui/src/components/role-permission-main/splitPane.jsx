import React from "react";
import SplitPane from "react-split-pane";

const SplitPaneVertical = ({ children }) => {
  return (
    <div>
      <SplitPane
        split="vertical"
        minSize={350}
        maxSize={-350}
        defaultSize="30%"
      >
        {children}
      </SplitPane>
    </div>
  );
};

export default SplitPaneVertical;
