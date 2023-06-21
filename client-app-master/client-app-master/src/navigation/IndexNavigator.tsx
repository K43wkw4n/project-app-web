import { View, Text } from "react-native";
import React from "react";
import { useAppSelector } from "../store/store";
import { AppNavigator } from "./Navigator";
import { AccountNavigator } from "./account/Account.Navigator";
import { NavigationContainer } from "@react-navigation/native";

export const IndexNavigator = () => {
  const { token } = useAppSelector((state) => state.account);

  return (
    <>
      <NavigationContainer>
        {!!token ? <AppNavigator /> : <AccountNavigator />}
      </NavigationContainer>
    </>
  );
};
