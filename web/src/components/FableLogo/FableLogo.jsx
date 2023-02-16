import React from "react";

export const FableLogo = () => {
  return (
    <div className={"w-1/2 h-1/2 bg-transparent"}>
      <img
        className={"object-contain w-auto h-auto"}
        src={new URL("fable_logo.png", import.meta.url)}
        width={614.5}
        height={453.25}
        alt={"Fable Logo"}
      ></img>
    </div>
  );
};
