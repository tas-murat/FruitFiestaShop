import React from "react"
import  Footer  from './Footer.tsx'
import {  Flex } from "@chakra-ui/react"
import  Main  from "./Main.tsx"
import Navigation  from "./Navigation.tsx"

export default function Layout() {
    return (
        <Flex direction="column" flex="1">
            <Navigation />
            <Main/>
            <Footer />
        </Flex>
    )
}