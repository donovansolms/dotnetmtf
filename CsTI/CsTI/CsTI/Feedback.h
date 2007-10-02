/*
 *  This part is responsible for sending the events to the c# client
 *  Check the CsTI_README.txt for exact instructions on using it.
 * 
 * 
 */

#pragma once

#include <vcclr.h>
//please correct the reference for your own computer
//TODO: make relative path
#include "C:\CsTI\TouchlibWrapper\TouchlibWrapper\TouchlibWrapper.h"
#include "TouchData.h"

using namespace TouchlibWrapper;

class Feedback : public CTouchlibWrapper::CFeedback 
{
public:
	Feedback( CsTI* p );
	void OnFingerDown(TouchData data);
	void OnFingerUp(TouchData data);
	void OnFingerUpdate(TouchData data);
	
private:
	gcroot< CsTI* > m_pManaged;
};
