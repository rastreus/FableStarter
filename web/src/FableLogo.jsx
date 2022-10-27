import React from "react";

export const FableLogo = () => {
  return (
    <div className={"w-full h-1/2 bg-transparent"}>
      <img
        className={"w-full h-full object-contain"}
        src={new URL("fable_logo.png", import.meta.url)}
        alt={"Fable Logo"}
      ></img>
    </div>
  );
};
