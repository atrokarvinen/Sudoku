import { ReactNode } from "react";
import "./Layout.css";

export interface LayoutProps {
  children: ReactNode;
}

export function Layout(props: LayoutProps) {
  return <div className="layout">{props.children}</div>;
}
