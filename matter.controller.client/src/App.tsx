import { useEffect, useState } from 'react';
import './App.css';
import { ReactFlow, Background, Controls } from '@xyflow/react';
import '@xyflow/react/dist/style.css';

interface MatterNode {
    id: number;
    position: { x: 0, y: 0 },
    data: { label: 'Hello' },
}

function App() {
    const [nodes, setNodes] = useState<MatterNode[]>();

    useEffect(() => {
        populateMatterNodes();
    }, []);

    return (
        <ReactFlow nodes={nodes}>
            <Background />
            <Controls />
        </ReactFlow>
    );

    async function populateMatterNodes() {
        const response = await fetch('/api/matter/nodes');
        if (response.ok) {
            const data = await response.json();

            console.log(`Fetched ${data.length} nodes`);
            console.log({ data });

            setNodes(data);
        }
    }
}

export default App;