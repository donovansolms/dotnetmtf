#include "stdafx.h"
#include "CsTI.h"
#include "Feedback.h"

using namespace TouchlibWrapper;

Feedback::Feedback( CsTI* p )
{
	m_pManaged = p;
}

//raises the actual fingerDown event from the unmanaged c++ touchlib
void Feedback::OnFingerDown(TouchData data)
{
	m_pManaged->raise_fingerDown(  data.ID, data.tagID, data.X, data.Y , data.angle, data.area, data.height, data.width, data.dX, data.dY );
}

//raises the actual fingerUp event from the unmanaged c++ touchlib
void Feedback::OnFingerUp(TouchData data)
{	
	m_pManaged->raise_fingerUp( data.ID, data.tagID, data.X, data.Y , data.angle, data.area, data.height, data.width, data.dX, data.dY);
}

//raises the actual fingerUpdate event from the unmanaged c++ touchlib
void Feedback::OnFingerUpdate(TouchData data)
{
	m_pManaged->raise_fingerUpdate( data.ID, data.tagID, data.X, data.Y , data.angle, data.area, data.height, data.width, data.dX, data.dY );
}
