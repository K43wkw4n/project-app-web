import { useFonts, Comfortaa_500Medium } from "@expo-google-fonts/comfortaa";
import AppLoading from "expo-app-loading";
import { Provider } from "react-redux";
import { store } from "./src/store/store";
import { AppNavigator } from "./src/navigation/Navigator";
import { IndexNavigator } from "./src/navigation/IndexNavigator";
//http://127.0.0.1:5030/api/Coupon/GetCoupon

import { LogBox } from "react-native";
LogBox.ignoreLogs(["Warning: ..."]); // Ignore log notification by message
LogBox.ignoreAllLogs(); //Ignore all log notifications

export default function App() {
  let [fontsLoaded] = useFonts({
    Comfortaa_500Medium,
  });

  if (!fontsLoaded) {
    return <AppLoading />;
  }

  return (
    <>
      <Provider store={store}>
        <IndexNavigator />
      </Provider>
    </>
  );
}
