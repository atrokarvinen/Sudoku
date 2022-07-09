import { render } from "@testing-library/react";
import { screen } from "@testing-library/dom";
import App from "./App";

it("renders without crashing", () => {
  render(<App />);

  expect(screen.getByText(/populate/i)).toBeInTheDocument();
});
