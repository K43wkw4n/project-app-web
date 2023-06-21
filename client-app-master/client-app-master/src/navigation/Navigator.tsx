import React from "react";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { HomeNavigator } from "./home/Home.Navigator";
import { AntDesign, Ionicons } from "@expo/vector-icons";
import { SettingScreen } from "../features/setting/screens/Setting.screen";

const Tab = createBottomTabNavigator();

export const AppNavigator = () => (
  <Tab.Navigator
    screenOptions={({ route }) => ({
      headerShown: false,
      tabBarIcon: ({ focused, color, size }) => {
        let iconName;

        if (route.name === "Home") {
          iconName = focused
            ? "ios-information-circle"
            : "ios-information-circle-outline";
        } else if (route.name === "Settings") {
          iconName = focused ? "ios-list" : "ios-list-outline";
        }

        return <AntDesign name={iconName as any} size={24} color={color} />;
      },
      tabBarActiveTintColor: "white",
      tabBarInactiveTintColor: "black",
      tabBarActiveBackgroundColor: "black",
      tabBarInactiveBackgroundColor: "white",
    })}
  >
    <Tab.Screen
      name="Home"
      options={{
        tabBarIcon: (e) => <AntDesign name="home" size={24} color={e.color} />,
        tabBarShowLabel: false,
      }}
      component={HomeNavigator}
    />
    <Tab.Screen
      name="setting"
      options={{
        tabBarIcon: (e) => (
          <Ionicons name="settings-outline" size={24} color={e.color} />
        ),
        tabBarShowLabel: false,
      }}
      component={SettingScreen}
    />
  </Tab.Navigator>
);
